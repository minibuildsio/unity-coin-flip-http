# Unity HTTP Request Example

This repo demonstrates how to use UnityWebRequest to make GET and POST requests. 

`Assets/Scripts/CoinFlipController.cs` containers methods `balance()` and `play()` which make requests to the endpoints `/balance` and `/play`. The play request passes a `PlayRequest` object in the body of the request serialised to json and return a `PlayResponse` again deserialised from json.

## Start the backend

The backend is a simple Python Flask app (see src/python/app.py) that will return the balance via the "/balance" endpoint and a play result via the "/play" endpoint.

```bash
# Windows
set FLASK_APP=src/python/app.py && python -m flask && python -m flask run

# Linux/Mac
export FLASK_APP=src/python/app.py && python -m flask && python -m flask run
```

