FROM node:16-buster-slim AS ui-build
WORKDIR /usr/src/app
COPY case/ ./case/
RUN cd case && npm install @angular/cli && npm install && npm run build 

FROM nginx:1.20.0-alpine AS server-build

COPY ./nginx.conf /etc/nginx/nginx.conf
COPY ./app.conf /etc/nginx/conf.d/default.conf

RUN adduser -S www-data -G www-data

RUN touch /var/run/nginx.pid && \
  mkdir /var/www && \
  chown -R www-data:www-data /var/run/nginx.pid && \
  chown -R www-data:www-data /var/cache/nginx && \
  chown -R www-data:www-data /var/www

USER www-data

WORKDIR /var/www

COPY --from=ui-build /usr/src/app/case/dist/case ./
COPY --from=ui-build /usr/src/app/case/node_modules ./

EXPOSE 8080
