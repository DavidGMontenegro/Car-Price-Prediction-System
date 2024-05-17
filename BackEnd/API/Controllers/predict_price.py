import pandas as pd
import joblib

def predict_car_price(year, kilometer, fuel_type, transmission, owner, make, model, color, seller_type, engine, drivetrain):
    # Cargar el modelo entrenado desde el archivo pkl
    model_data = joblib.load('modelo_entrenado.pkl')
    model = model_data['model']

    # Crear un DataFrame con los datos del nuevo coche
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

    # Codificar el nuevo coche utilizando la misma codificación que se usó durante el entrenamiento
    nuevo_coche_encoded = pd.get_dummies(nuevo_coche)

    # Asegurarse de que el conjunto de datos del nuevo coche tenga las mismas columnas y en el mismo orden que el conjunto de datos de entrenamiento
    nuevo_coche_encoded = nuevo_coche_encoded.reindex(columns=model_data['X_columns'], fill_value=0)

    # Realizar la predicción del precio del nuevo coche
    precio_predicho = model.predict(nuevo_coche_encoded.values.reshape(1, -1)) * 0.011
    return precio_predicho[0]
