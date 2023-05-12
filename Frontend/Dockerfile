# Use an official Node.js runtime as a parent image
FROM node:latest
# Set the working directory to /app
WORKDIR /app
# Copy package.json and package-lock.json to the working directory
COPY package*.json ./
# Install dependencies
RUN npm install
# Copy the rest of the application files to the working directory
COPY . .
# Build the React application
RUN npm run build
# Set the environment variable for the port
ENV PORT=http://host.docker.internal:4000
# Expose the port
EXPOSE 3000
# Run the command to start the server
CMD ["npm", "start"]
