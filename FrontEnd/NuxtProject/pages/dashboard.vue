<template>
  <div class="graph-container">
    <div class="header">
      <h1>Car Analytics Dashboard</h1>
      <p>Explore insights into car data with interactive graphs.</p>
    </div>
    <el-select-v2
      v-model="selectedGraph"
      :options="options"
      placeholder="Please select"
      style="width: 240px"
    />
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
      selectedGraph: "YearDistribution",
      options: [
        { value: "YearDistribution", label: "Year Price Distribution" },
        { value: "PriceTrendByYear", label: "Price Trend by Year" },
        {
          value: "PriceDistributionByMake",
          label: "Price Distribution by Make",
        },
        {
          value: "TransmissionTypeDistribution",
          label: "Transmission Type Distribution",
        },
        { value: "FuelTypeDistribution", label: "Fuel Type Distribution" },
      ],
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.graph-container {
  background-color: $color-background;
  border-radius: 8px;
  margin: 5vh auto 40vh auto;
  display: flex;
  flex-direction: column;
  align-items: center;
  max-height: 65vh;
  max-width: 90%;
}

.el-select-v2 {
  background-color: $color-primary;
  border-color: $color-primary; // Change border color
  .el-select-v2__selection {
    color: white; // Adjust text color for contrast
  }
  .el-select-v2__caret {
    fill: $color-primary; // Change caret color
  }
}

.header {
  text-align: center;
  margin-bottom: 5vh;
  color: $color-secondary;

  h1 {
    font-size: 36px;
    margin-bottom: $spacing-medium;
    color: $color-primary;
  }
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
