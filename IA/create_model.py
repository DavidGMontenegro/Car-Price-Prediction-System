import pandas as pd
from sklearn.ensemble import RandomForestRegressor
from sklearn.model_selection import train_test_split
import joblib

# Cargar los datos desde un archivo CSV
df = pd.read_csv("car-dataset/Car details v4.csv")

# Seleccionar características y variable objetivo
desired_columns = ['Make', 'Model', 'Price', 'Year', 'Kilometer', 'Fuel Type',
                    'Transmission', 'Color', 'Owner', 'Seller Type', 'Engine',
                    'Drivetrain', 'Length', 'Width', 'Height', 'Seating Capacity',
                    'Fuel Tank Capacity']

# Remove rows with missing values before encoding
df.dropna(subset=desired_columns, inplace=True)

X = df[desired_columns].drop('Price', axis=1)
y = df['Price']

# Codificar características categóricas
X_encoded = pd.get_dummies(X)

# Dividir los datos en conjuntos de entrenamiento y prueba
X_train, X_test, y_train, y_test = train_test_split(X_encoded, y, test_size=0.2, random_state=42)

# Entrenar un modelo de regresión (por ejemplo, un modelo de bosque aleatorio)
model = RandomForestRegressor(n_estimators=100, random_state=42)
model.fit(X_train, y_train)

# Guardar el modelo entrenado en un archivo con Joblib
joblib.dump({'model': model, 'X_columns': X_train.columns}, 'modelo_entrenado.pkl')
