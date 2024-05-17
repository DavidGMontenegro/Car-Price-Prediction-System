from flask import Flask, request, jsonify
from flask_cors import CORS
import joblib
import pandas as pd

app = Flask(__name__)
CORS(app)  # Habilita CORS en la aplicación

def predict_car_price(year, kilometer, fuel_type, transmission, owner, make, model, color, seller_type, engine, drivetrain):
    model_data = joblib.load('modelo_entrenado.pkl')
    model = model_data['model']

    nuevo_coche = pd.DataFrame({
        "Year": [year],
        "Kilometer": [kilometer],
        "Fuel Type": [fuel_type],
        "Transmission": [transmission],
        "Owner": [owner],
        "Make": [make],
        "Model": [model],
        "Color": [color],
        "Seller Type": [seller_type],
        "Engine": [engine],
        "Drivetrain": [drivetrain]
    })

    nuevo_coche_encoded = pd.get_dummies(nuevo_coche)
    nuevo_coche_encoded = nuevo_coche_encoded.reindex(columns=model_data['X_columns'], fill_value=0)

    precio_predicho = model.predict(nuevo_coche_encoded.values.reshape(1, -1)) * 0.011
    return precio_predicho[0]

@app.route('/predict', methods=['POST'])
def predict():
    data = request.form
    print(data)
    prediction = predict_car_price(
        data['year'],
        data['kilometer'],
        data['fuel_type'],
        data['transmission'],
        data['owner'],
        data['make'],
        data['model'],
        data['color'],
        data['seller_type'],
        data['engine'],
        data['drivetrain']
    )
    return jsonify({"predicted_price": prediction})

if __name__ == '__main__':
    app.run(debug=True)
