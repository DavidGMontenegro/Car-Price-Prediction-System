import pandas as pd
import json
import sys

from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestRegressor

def predict_price(parameters, csv_path):
    # Cargar los datos desde un archivo CSV
    df = pd.read_csv(csv_path)

    # Seleccionar características y variable objetivo
    desired_columns = ['Make', 'Model', 'Price', 'Year', 'Kilometer', 'Fuel Type',
                       'Transmission', 'Color', 'Owner', 'Seller Type', 'Engine',
                       'Drivetrain', 'Length', 'Width', 'Height', 'Seating Capacity',
                       'Fuel Tank Capacity']
    df.dropna(subset=desired_columns, inplace=True)

    X = df[desired_columns]
    y = df['Price']

    # Codificar características categóricas
    X_encoded = pd.get_dummies(X)

    # Dividir los datos en conjuntos de entrenamiento y prueba
    X_train, X_test, y_train, y_test = train_test_split(X_encoded, y, test_size=0.2, random_state=42)

    # Entrenar un modelo de regresión (por ejemplo, un modelo de bosque aleatorio)
    model = RandomForestRegressor(n_estimators=100, random_state=42)
    model.fit(X_train, y_train)

    nuevo_coche = pd.DataFrame([parameters])

    # Codificar el nuevo coche utilizando la misma codificación que se usó en el conjunto de datos de entrenamiento
    nuevo_coche_encoded = pd.get_dummies(nuevo_coche)

    # Asegurarse de que el conjunto de datos del nuevo coche tenga las mismas columnas y en el mismo orden que el conjunto de datos de entrenamiento
    nuevo_coche_encoded = nuevo_coche_encoded.reindex(columns=X_train.columns, fill_value=0)

    # Realizar la predicción del precio del nuevo coche
    precio_predicho = model.predict(nuevo_coche_encoded) * 0.011
    return precio_predicho[0]

if __name__ == "__main__":
    json_data = sys.argv[1]
    parameters = json.loads(json_data)
    
    csv_path = "car-dataset/Car details v4.csv"
    predicted_price = predict_price(parameters, csv_path)
    print(predicted_price)
