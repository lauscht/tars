FROM node:10 AS ui-build
WORKDIR /usr/src/app
COPY case/ ./case/
RUN cd case && npm install @angular/cli && npm install && npm run build

FROM node:10 AS server-build
WORKDIR /root/
COPY --from=ui-build /usr/src/app/case/dist ./case/dist
COPY package*.json ./
RUN npm install
COPY server.js .

EXPOSE 3080

CMD ["node", "server.js"]
