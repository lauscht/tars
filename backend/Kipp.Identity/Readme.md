# Setup Certificate

Create Key and Certificate

    openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout tars.key -out tars.crt -subj "/CN=tars.lauscht.com" -days 3650

Create X509 Container

    openssl pkcs12 -export -out tars.pfx -inkey tars.key -in tars.crt -certfile tars.crt

Keep it safe an hidden.