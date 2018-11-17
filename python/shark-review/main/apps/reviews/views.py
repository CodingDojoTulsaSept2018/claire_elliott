from django.shortcuts import render, redirect
from django.contrib import messages
import bcrypt
from .models import User, Shark, Review
import bcrypt

def index(request):
    return render(request, "index.html")

def login(request):
    # find if user exists in database and pull the entire user obj out
    errors = User.objects.log_validator(request.POST)

    if len(errors):
        for key, value in errors.items():
            messages.error(request, value)
        # redirect the user back to the form to fix the errors
        return redirect('/')
    else:
        request.session['email'] = request.POST['log_email']
        logged_in = User.objects.get(email=request.POST['log_email'])
        request.session['welcome'] = logged_in.first_name

        return redirect('/dashboard')
        
        
def register(request):
    errors = User.objects.reg_validator(request.POST)
        # check if the errors object has anything in it
    if len(errors):
        # if the errors object contains anything, loop through each key-value pair and make a flash message
        for key, value in errors.items():
            messages.error(request, value)
        # redirect the user back to the form to fix the errors
        return redirect('/')
    else:
        new_user = User.objects.create(first_name=request.POST['first_name'],last_name=request.POST['last_name'], email=request.POST['email'], password=bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt()))
        
        # saving session data to reference the client in the future
        request.session['email'] = request.POST['email']
        request.session['welcome'] = request.POST['first_name']

        # !!!don't use the following 4 lines!!!
        # I have a diff Django version that doesn't play nice w/ bcrypt
        str_pw = str(new_user.password)
        sliced_pw = str_pw[2:len(str_pw)-1]
        new_user.password = sliced_pw
        new_user.save()

        return redirect('/dashboard')

def dashboard(request):
    # checks to see if client already has registration/login session credentials
    # if they don't, redirects them to login/reg to sign up or log in
    # this keeps unregistered users from accessing members-only pages
    if 'email' not in request.session:
        return redirect('/')
    else:
        # adds all shark objects to django context dict
        context = {
            'allSharks': Shark.objects.all()
        }
        return render(request, "dashboard.html", context)

def create_shark(request):
    errors = Shark.objects.shark_validator(request.POST)

    if len(errors):
        for key, value in errors.items():
            messages.error(request, value)

        return redirect('/dashboard')
    else:
        # creates Shark obj
        # you can add the foreignkey obj right into the create function. But not with manytomany
        Shark.objects.create(name=request.POST['name'], url=request.POST['url'], creator = User.objects.get(email=request.session['email']))

        return redirect('/dashboard')

def show_one(request,id):
    if 'email' not in request.session:
            return redirect('/')
    else:
        context = {
            'oneShark': Shark.objects.get(id=id),
            'reviews': Review.objects.filter(shark_reviewed=Shark.objects.get(id=id)),
        }

        return render(request, 'show_one.html', context)

def create_review(request):
    errors = Review.objects.review_validator(request.POST)

    if len(errors):
        for key, value in errors.items():
            messages.error(request, value)

        return redirect('/sharks/{}'.format(request.POST['shark_id']))
    else:
        new_review = Review.objects.create(comment=request.POST['comment'], reviewer=User.objects.get(email=request.session['email']))
        # you cannot add to a many to many field within the create function.
        # make and assign the created obj, then add the associated obj to it and save
        shark = Shark.objects.get(id=request.POST['shark_id'])
        shark.has_reviews.add(new_review)
        shark.save()

        return redirect('/sharks/{}'.format(request.POST['shark_id']))

def delete_shark(request,id):
    this_user = User.objects.get(email=request.session['email'])
    this_shark = Shark.objects.get(id=id)
    if 'email' not in request.session:
            return redirect('/')
    else:
        if this_user.id != this_shark.creator.id:
            return redirect('/sharks/{}'.format(id))
        else:
            this_shark.delete()
            return redirect('/dashboard')

def logout(request):
    request.session.clear()

    return redirect('/')