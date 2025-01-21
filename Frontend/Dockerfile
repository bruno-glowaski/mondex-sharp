ARG NODE_VERSION=22.8.0
ARG PORT=3000

FROM node:${NODE_VERSION}-slim as base

FROM base as build
WORKDIR /src
COPY --link ./Frontend/package.json ./Frontend/package-lock.json .
RUN npm ci
COPY --link ./Frontend .
RUN npm run build

FROM base
WORKDIR /app
ENV PORT=$PORT
ENV NODE_ENV=production
COPY --from=build /src/.output .
CMD [ "node", "server/index.mjs" ]
