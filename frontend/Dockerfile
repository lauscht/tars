FROM node:16-buster-slim AS ui-build
WORKDIR /usr/src/app
COPY case/ ./case/
RUN cd case && npm install @angular/cli && npm install && npm run build 

FROM node:16-buster-slim AS server-build
WORKDIR /root/
COPY --from=ui-build /usr/src/app/case/ ./
COPY --from=ui-build /usr/src/app/case/package*.json ./
RUN npm install
COPY ./server.js ./

EXPOSE 4200

CMD ["node", "server.js"]
