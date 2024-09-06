import os
import time
import json
import logging
import random
from datetime import datetime
import threading
from azure.iot.device import IoTHubModuleClient, Message, exceptions, MethodResponse

logging.basicConfig(level=logging.INFO)

CONNECTION_STRING = os.getenv('IOTHUB_MODULE_CONNECTION_STRING')

CURRENT_STATE = "ON"
INTERVAL = 15

def create_client(connection_string):
    try:
        client = IoTHubModuleClient.create_from_connection_string(connection_string)
        client.connect()
        logging.info("Connected to Azure IoT Hub")
        return client
    except exceptions.ConnectionFailedError as e:
        logging.error(f"Failed to connect: {e}")
        return None
    except Exception as e:
        logging.error(f"Unexpected error during connection: {e}")
        return None


def send_message(client, data):
    try:
        message = Message(json.dumps(data))
        client.send_message_to_output(message, "output1")
        current_time = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        logging.info(f"Message successfully sent at {current_time}: {message}")
    except exceptions.ConnectionDroppedError as e:
        logging.error(f"Connection dropped: {e}")
        try:
            client.connect()
            logging.info("Reconnected to Azure IoT Hub")
        except Exception as reconnect_exception:
            logging.error(f"Failed to reconnect: {reconnect_exception}")
            time.sleep(10)
    except Exception as e:
        logging.error(f"Unexpected error: {e}")



def send_state(client):
    global CURRENT_STATE
    data = {
        "current_state": CURRENT_STATE
    }
    try:
        message = Message(json.dumps(data))
        client.send_message_to_output(message, "output1")
        current_time = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        logging.info(f"Device state at {current_time}: {message}")
    except exceptions.ConnectionDroppedError as e:
        logging.error(f"Connection dropped: {e}")
        try:
            client.connect()
            logging.info("Reconnected to Azure IoT Hub")
        except Exception as reconnect_exception:
            logging.error(f"Failed to reconnect: {reconnect_exception}")
            time.sleep(10)
    except Exception as e:
        logging.error(f"Unexpected error: {e}")

def send_interval(client):
    global INTERVAL
    data = {
        "current_interval": INTERVAL
    }
    try:
        message = Message(json.dumps(data))
        client.send_message_to_output(message, "output1")
        current_time = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        logging.info(f"Device interval at {current_time}: {message}")
    except exceptions.ConnectionDroppedError as e:
        logging.error(f"Connection dropped: {e}")
        try:
            client.connect()
            logging.info("Reconnected to Azure IoT Hub")
        except Exception as reconnect_exception:
            logging.error(f"Failed to reconnect: {reconnect_exception}")
            time.sleep(10)
    except Exception as e:
        logging.error(f"Unexpected error: {e}")



def handle_method_request(method_request):
    global CURRENT_STATE, INTERVAL

    try:

        if method_request.name == "ON":
            logging.info("Received ON command")
            CURRENT_STATE = "ON"
            logging.info("Device is now ON")
            resp_payload = {"Response": "The device is now on"}


        elif method_request.name == "OFF":
            logging.info("Received OFF command")
            CURRENT_STATE = "OFF"
            logging.info("The device is now OFF")
            resp_payload = {"Response": "The device is now off"}

        elif method_request.name == "STATE":
            logging.info("Received STATE command")
            logging.info(f"The device state is {CURRENT_STATE}")
            resp_payload = {"currentState": f"{CURRENT_STATE}"}
            send_state(module_client)

        elif method_request.name == "getINTERVAL":
            logging.info("Received getINTERVAL command")
            logging.info(f"The device interval is {INTERVAL}")
            resp_payload = {"interval": f"{INTERVAL}"}
            send_interval(module_client)

        elif method_request.name == "setINTERVAL":
            logging.info("Received INTERVAL command")
            logging.info(f"The device interval is {method_request.payload}")
            resp_payload = {"Response": f"The device interval is changed to {method_request.payload} from {INTERVAL}"}
            INTERVAL = method_request.payload

        else:
            resp_payload = {"Response": "Unknown method"}

        resp_status = 200
        module_client.send_method_response(
            MethodResponse(method_request.request_id, status=resp_status, payload=resp_payload))

    except Exception as e:
        logging.error(f"Error handling method request: {e}")


def fetchSensorData():
    sensors = [
        {
            "patient": "Trajko Trajkov",
            "measureType": "Heart Rate",
            "embg": "9883176722815",
            "value": round(random.uniform(20, 150), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Trajko Trajkov",
            "measureType": "Oxygen Saturation",
            "embg": "9883176722815",
            "value": round(random.uniform(87, 100), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Suzana Trajkova",
            "measureType": "Heart Rate",
            "embg": "3114854314589",
            "value": round(random.uniform(20, 150), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Suzana Trajkova",
            "measureType": "Oxygen Saturation",
            "embg": "3114854314589",
            "value": round(random.uniform(87, 100), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Petar Trajkov",
            "measureType": "Heart Rate",
            "embg": "8126523275617",
            "value": round(random.uniform(20, 150), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Petar Trajkov",
            "measureType": "Oxygen Saturation",
            "embg": "8126523275617",
            "value": round(random.uniform(87, 100), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Pero Trajkov",
            "measureType": "Heart Rate",
            "embg": "2750436624510",
            "value": round(random.uniform(20, 150), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        },
        {
            "patient": "Pero Trajkov",
            "measureType": "Oxygen Saturation",
            "embg": "2750436624510",
            "value": round(random.uniform(87, 100), 2),
            "time": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),
        }
    ]
    return sensors

def main():
    global module_client
    module_client = create_client(CONNECTION_STRING)
    if not module_client:
        return

    module_client.on_method_request_received = handle_method_request


    while True:
        if CURRENT_STATE == "ON":
            logging.info("Device is ON")
            data = fetchSensorData()
            for sensor in data:
                send_message(module_client, sensor)
        elif CURRENT_STATE == "OFF":
            logging.info("Device is in OFF state. Not sending data.")
        else:
            logging.info("Device is ON and not PAUSED. Sending data as usual.")

        time.sleep(INTERVAL)


if __name__ == '__main__':
    main()
