# Use the official Node.js image
FROM node:20-alpine

# Install system dependencies needed for some npm packages
RUN apk add --no-cache python3 make g++

# Set the working directory inside the container
WORKDIR /app

# Ensure we're in development mode and local binaries are in PATH
ENV NODE_ENV=development
ENV PATH=/app/node_modules/.bin:$PATH

# Copy package files
COPY package.json ./

# Install all dependencies including dev dependencies
RUN npm install
#  && \
#     echo "=== Checking devDependencies installation ===" && \
#     echo "vite location:" && ls -la node_modules/.bin/vite 2>/dev/null || echo "vite not found" && \
#     echo "tsc location:" && ls -la node_modules/.bin/tsc 2>/dev/null || echo "tsc not found" && \
#     echo "eslint location:" && ls -la node_modules/.bin/eslint 2>/dev/null || echo "eslint not found" && \
#     echo "tailwindcss location:" && ls -la node_modules/.bin/tailwindcss 2>/dev/null || echo "tailwindcss not found" && \
#     echo "=== DevDependencies check complete ==="

# Copy the rest of the application code
COPY . .

# Expose the port that Vite dev server runs on
EXPOSE 5173

# Start the development server
CMD ["npm", "run", "dev"]

