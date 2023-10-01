# Stage 1: Build the React app
FROM node:17 as build

# Set the working directory in the container
WORKDIR /app

# Copy the package.json and package-lock.json files to the container
COPY package.json ./
COPY package-lock.json ./

# Install dependencies
RUN npm install

# Copy the remaining app source code to the container
COPY . .

# Build the React app
RUN npm run build

# Stage 2: Use a lightweight Nginx image as the final base image
FROM nginx:alpine

# Copy the built React app from the build container to the Nginx web server directory
COPY --from=build /app/build /usr/share/nginx/html

# Expose port for the Nginx web server
EXPOSE 3000

# Start the Nginx web server (it runs in the foreground)
CMD ["nginx", "-g", "daemon off;"]
