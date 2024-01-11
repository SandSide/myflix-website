# myflix-website
 
## Description 

Client web application for Myflix project, a simple prototype video streaming website created using microservice artitecture.

## Features

- **Login**: Users can securely login into their accounts. 
- **Register New Account**: New user can create new accounts to have access to website features.
- **View Video Catalogue**: Users can view a catalogue of videos.
- **Watch Video**: Users can watch videos from the catalogue.

## Deployment

### Using Docker:
1. Create an docker image from `Dockerfile` in `/src` folder.

```bash
docker build -t myflix-website .
```

2. Run the docker image.

```bash
docker run -p 5000:5000 myflix-website
```

### Using Visual Studio:
1. Open project solution in Visual Studio.
2. Build project in Visual Studio.