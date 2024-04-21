<template>
  <div class="graph-container">
    <select v-model="selectedGraph" class="select-graph">
      <option value="YearDistribution">Distribución por año</option>
      <option value="PriceTrendByYear">Tendencia de precios</option>
      <option value="PriceDistributionByMake">
        Distribución de precios por marca
      </option>
      <option value="TransmissionTypeDistribution">
        Distribución de tipos de transmisión
      </option>
      <option value="FuelTypeDistribution">
        Distribución de tipos de combustible
      </option>
    </select>
    <component :is="selectedGraph" class="graph" />
  </div>
</template>

<script>
import YearDistribution from "~/components/graphs/YearDistribution.vue";
import PriceTrendByYear from "~/components/graphs/PriceTrendByYear.vue";
import PriceDistributionByMake from "~/components/graphs/PriceDistributionByMake.vue";
import TransmissionTypeDistribution from "~/components/graphs/TransmissionTypeDistribution.vue";
import FuelTypeDistribution from "~/components/graphs/FuelTypeDistribution.vue";

export default {
  components: {
    YearDistribution,
    PriceTrendByYear,
    PriceDistributionByMake,
    TransmissionTypeDistribution,
    FuelTypeDistribution,
  },
  data() {
    definePageMeta({
      middleware: "auth",
    });

    return {
      selectedGraph: "YearDistribution", // Por defecto muestra la distribución por año
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.graph-container {
  background-color: $color-background;
  padding: $spacing-medium;
  border-radius: 8px;
  margin: 5vh auto 30vh auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  max-height: 65vh;
  /* Limitar el ancho del contenedor del gráfico */
  max-width: 90%; /* Puedes ajustar este valor según tus necesidades */
}

.select-graph {
  font-size: $font-size-small;
  padding: $spacing-small;
  border-radius: 5px;
  background-color: $color-primary;
  color: black;
  cursor: pointer;
  transition: border-color 0.3s ease;

  &:focus {
    background-color: $color-secondary;
  }
}

.graph {
  margin-top: $spacing-medium;
  /* Limitar el ancho del gráfico */
  max-width: 100%; /* Asegura que el gráfico ocupe todo el ancho disponible */
}
</style>
