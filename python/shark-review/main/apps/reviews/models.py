from __future__ import unicode_literals
from django.db import models
import bcrypt
import re
EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
URL_REGEX = re.compile(r'(?:http\:|https\:)?\/\/.*\.(?:png|jpg|jpeg)')

class UserManager(models.Manager):
    def reg_validator(self, postData):
        errors = {}
        if len(postData['first_name']) < 2 or len(postData['first_name']) > 45:
            errors['first_name'] = "First Name should be between 2 and 45 characters long."
        if len(postData['last_name']) < 2 or len(postData['last_name']) > 45:
            errors['last_name'] = "Last Name should be between 2 and 45 characters long."
        if not EMAIL_REGEX.match(postData['email']):
            errors['email'] = "Email is not valid."
        check = User.objects.filter(email=postData['email'])
        if check:
            errors['login'] = "Email address is already registered."
        if len(postData['password']) < 8:
            errors['password'] = "Password must be at least 8 characters long."
        if postData['password'] != postData['confirm_password']:
            errors['password'] = "Password do not match."

        return errors

    def log_validator(self, postData):
        errors = {}
        check = User.objects.filter(email=postData['log_email'])
        if not check:
            errors['login'] = "Email has not been registered."
        else:
            if not bcrypt.checkpw(postData['log_password'].encode(), check[0].password.encode()):
                errors['login'] = "Email and password don't match."
        
        return errors

class SharkManager(models.Manager):
    def shark_validator(self, postData):
        errors = {}
        if len(postData['name']) < 2:
            errors['name'] = "Shark name must be at least 2 characters long."
        if not URL_REGEX.findall(postData['url']) :
            errors['url'] = "Image link is not valid."
        
        return errors

class ReviewManager(models.Manager):
    def review_validator(self, postData):
        errors = {}
        if len(postData['comment']) < 1:
            errors['comment'] = "Comment cannot be blank."
    
        return errors

class User(models.Model):
    first_name = models.CharField(max_length=45)
    last_name = models.CharField(max_length=45)
    email = models.CharField(max_length=255)
    password = models.TextField()
    created_at = models.DateTimeField(auto_now_add = True)
    updated_at = models.DateTimeField(auto_now = True)

    objects = UserManager()

    def __repr__(self):
        return "<User object: {} {}>".format(self.first_name, self.email)

class Shark(models.Model):
    name = models.CharField(max_length=255)
    url = models.TextField()
    creator = models.ForeignKey(User, related_name="sharks_created", on_delete=models.CASCADE)
    created_at = models.DateTimeField(auto_now_add = True)
    updated_at = models.DateTimeField(auto_now = True)

    objects = SharkManager()

class Review(models.Model):
    comment = models.CharField(max_length=255)
    reviewer = models.ForeignKey(User, related_name="has_reviews", on_delete=models.CASCADE)
    shark_reviewed = models.ManyToManyField(Shark, related_name="has_reviews")
    created_at = models.DateTimeField(auto_now_add = True)
    updated_at = models.DateTimeField(auto_now = True)

    objects = ReviewManager()