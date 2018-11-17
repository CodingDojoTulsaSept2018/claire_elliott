from django.contrib import admin
from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^login$', views.login),
    url(r'^register$', views.register),
    url(r'^dashboard$', views.dashboard),
    url(r'^sharks/create$', views.create_shark),
    url(r'^sharks/(?P<id>\d+)$', views.show_one),
    url(r'^sharks/(?P<id>\d+)/delete$', views.delete_shark),
    url(r'^reviews/create$', views.create_review),
    url(r'^logout$', views.logout),
]
