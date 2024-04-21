<template>
  <canvas id="priceTrendByYearChart"></canvas>
</template>

<script>
import axios from "axios";
import Chart from "chart.js/auto";
import { priceByYearEndPoint } from "~/constants/endpoints";

export default {
  mounted() {
    this.fetchPriceTrendByYear();
  },
  methods: {
    async fetchPriceTrendByYear() {
      try {
        const response = await axios.get(priceByYearEndPoint);
        const priceTrendByYearData = {
          labels: Object.keys(response.data),
          data: Object.values(response.data),
        };
        console.log("Price Trend By Year Data:", priceTrendByYearData);
        this.updateChart(priceTrendByYearData);
      } catch (error) {
        console.error("Error fetching price trend by year data:", error);
      }
    },
    updateChart(data) {
      const priceTrendByYearChart = new Chart(document.getElementById('priceTrendByYearChart'), {
        type: 'line',
        data: {
          labels: data.labels,
          datasets: [{
            label: "Price Trend By Year",
            backgroundColor: '#f96182',
            borderColor: '#f96182',
            data: data.data
          }]
        },
        options: {}
      });
    },
  },
};
</script>
