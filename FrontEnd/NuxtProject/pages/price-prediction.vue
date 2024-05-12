<template>
  <div>
    <h1>Formulario para Datos del Coche</h1>
    <form @submit.prevent="submitForm">
      <!-- Utiliza v-for para generar dinámicamente los select -->
      <div v-for="(values, columnName) in columnValues" :key="columnName">
        <label :for="columnName">{{ columnName }}:</label>
        <select :id="columnName" v-model="carData[columnName]" required>
          <option v-for="value in values" :value="value">{{ value }}</option>
        </select>
      </div>
      <button type="submit">Enviar Datos</button>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      carData: {},
      columnValues: {}, // Almacena los valores únicos de cada columna
    };
  },
  async created() {
    // Cargar los valores de las columnas al inicializar el componente
    await this.loadColumnValues();
  },
  methods: {
    async loadColumnValues() {
      try {
        // Llamar al backend para obtener los valores de las columnas
        const response = await this.$axios.get("/api/car-data/column-values");
        this.columnValues = response.data;
      } catch (error) {
        console.error("Error loading column values:", error);
      }
    },
    async submitForm() {
      // Método para enviar el formulario al backend
    },
  },
};
</script>

<style scoped>
/* Estilos específicos de la vista */
</style>
