# Stage 1: Build the Angular app
FROM node:18 AS build
WORKDIR /app

COPY mlt.web/package*.json ./
RUN npm install

COPY mlt.web/ ./
RUN npm run build -- --configuration=production

# Stage 2: Serve the app with Nginx
FROM nginx:stable-alpine
COPY --from=build /app/dist/mlt.web /usr/share/nginx/html

# Copy custom Nginx configuration
COPY mlt.web/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]