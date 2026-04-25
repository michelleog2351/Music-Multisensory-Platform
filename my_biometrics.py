#################################################################
# Name: Michelle Ogunade                                        #
# Date: 18.01.2026                                              #
# Program Title: My Flask Program - Importing Fitbit Data Files #
#################################################################

from flask import Flask, jsonify 
import requests

app = Flask(__name__)

FITBIT_TOKEN = "ENTER YOUR FITBIT TOKEN HERE"

@app.route("/api/biometric")
def get_biometric():
    headers = {
        "Authorization": f"Bearer {FITBIT_TOKEN}"
    }
    
    hr = requests.get(
        "https://api.fitbit.com/1/user/-/activties/heart/date/today/1d.json",
        headers=headers
        ).json()
    
    steps = requests.get(
        "https://api.fitbit.com/1/user/-/activties/heart/date/today/1d.json",
        headers=headers
        ).json()
    
    try:
        resting_hr = hr["activities-heart"][0]["value"].get("restingHeartRate", 70)
        steps_val = int(steps["activities-steps"][0]["value"])
    except:
        resting_hr = 70
        steps_val = 0
    
    return jsonify({
            "averageRestingHeartRate": resting_hr,
            "averageDailySteps": steps_val,
            "averageActiveMinutes": 20,
            "averageBreathingRate": resting_hr / 4,
            "averageHeartRateVariability": 55,
            "isCalmState": resting_hr < 70,
            "capturedAt": "2026-04-16T12:00:00"
        })

if __name__== "__main__":
    app.run(host="0.0.0.0", port=5000)
            

# @app.route("/")
# def index():
#     return "Biometric API running!"
