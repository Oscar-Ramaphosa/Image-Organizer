# Image Organizer – Full Stack Application

A full-stack image management application built with ASP.NET Core (.NET 8) and JavaScript frontend.

The application allows users to upload images, automatically extract metadata, preview them as thumbnails and filter images efficiently using multiple criteria.

## Project Motivation

This project was inspired by a real-world use case.

A family member was working on a project that involved managing a large number of images, which made organization and searching difficult. I used this opportunity to design and build a full-stack solution that demonstrates how image data can be stored, processed, and filtered efficiently in a real application.

The goal was to go beyond basic CRUD examples and showcase practical full-stack development skills.

## Features

- Upload images through a REST API

- Thumbnail previews of uploaded images

- Automatic metadata extraction:

File name

File type

Image orientation (Landscape / Portrait)

File size

Upload timestamp

- Frontend filtering:

Search by file name

Filter by file type

Filter by image orientation

- Delete uploaded images

🧩 Clean separation of concerns (Controller → Service → Data)

 ## Tech Stack
Backend

ASP.NET Core Web API (.NET 8)

Entity Framework Core

SQL Server

ImageSharp

Swagger / OpenAPI

## Frontend

HTML

CSS

Vanilla JavaScript (ES6)
