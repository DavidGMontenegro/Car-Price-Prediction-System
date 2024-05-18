from flask import Flask, request, jsonify
from flask_cors import CORS
import joblib
import pandas as pd

app = Flask(__name__)
CORS(app)  # Habilita CORS en la aplicación

# Función para predecir el precio del automóvil
def predict_car_price(year, kilometer, fuel_type, transmission, owner, make, model, color, seller_type, engine, drivetrain):
    # Cargar el modelo entrenado
    model_data = joblib.load('modelo_entrenado.pkl')
    model = model_data['model']

    # Crear un DataFrame con los datos del nuevo automóvil
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

    # Codificar las características categóricas
    nuevo_coche_encoded = pd.get_dummies(nuevo_coche)
    nuevo_coche_encoded = nuevo_coche_encoded.reindex(columns=model_data['X_columns'], fill_value=0)

    # Predecir el precio del automóvil
    precio_predicho = model.predict(nuevo_coche_encoded.values.reshape(1, -1)) * 0.011
    return precio_predicho[0]

# Ruta para la predicción de precios
@app.route('/predict', methods=['POST'])
def predict():
    data = request.form
    # Obtener los datos del formulario y realizar la predicción
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
    # Devolver el precio predicho en formato JSON
    return jsonify({"predicted_price": prediction})

if __name__ == '__main__':
    app.run(debug=True)
