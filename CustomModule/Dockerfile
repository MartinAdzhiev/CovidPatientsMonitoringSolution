FROM python:3.9-slim

WORKDIR /usr/src/app

COPY . .

RUN pip install --no-cache-dir azure-iot-device azure-identity azure-keyvault-secrets

EXPOSE 80

CMD ["python", "./CustomModule.py"]