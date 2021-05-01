from flask import Flask, request, session, Response
import os
import random

app = Flask(__name__)
app.balance = 1000000

@app.route('/balance')
def balance():
    return { 'balance': app.balance }

@app.route('/play', methods=['POST'])
def play():
    if request.json is None or request.json['game'] != 'coin-flip':
        return Response("Invalid request", 400)

    app.balance -= 1

    heads = random.randint(0, 1) == 1
    
    if heads:
        app.balance += 2

    return { 'balance': app.balance, 'result': 'heads' if heads else 'tails', 'payout': 2 if heads else 0 }