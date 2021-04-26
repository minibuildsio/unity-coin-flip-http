from flask import Flask, request, session
import os
import random

app = Flask(__name__)
app.balance = 1000000

@app.route('/balance')
def balance():
    return { 'balance': app.balance }

@app.route('/play', methods=['POST'])
def play():
    app.balance -= 1

    heads = random.randint(0, 1) == 1
    
    if heads:
        app.balance += 1

    return { 'balance': app.balance, 'result': 'heads' if heads else 'tails', 'payout': 1 if heads else 0 }