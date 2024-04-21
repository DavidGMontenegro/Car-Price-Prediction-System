<template>
  <canvas id="transmissionTypeChart"></canvas>
</template>

<script>
import axios from "axios";
import Chart from "chart.js/auto";
import { transmissionTypeEndPoint } from "~/constants/endpoints";

export default {
  mounted() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const response = await axios.get(transmissionTypeEndPoint);
        const transmissionTypeDistributionData = {
          labels: Object.keys(response.data),
          data: Object.values(response.data),
        };
        console.log(
          "Transmission Type Distribution Data:",
          transmissionTypeDistributionData
        );
        this.createChart(transmissionTypeDistributionData);
      } catch (error) {
        console.error(
          "Error fetching transmission type distribution data:",
          error
        );
      }
    },
    createChart(data) {
      const ctx = document
        .getElementById("transmissionTypeChart")
        .getContext("2d");
      new Chart(ctx, {
        type: "doughnut",
        data: {
          labels: data.labels,
          datasets: [
            {
              label: "Distribución de tipos de transmisión",
              backgroundColor: [
                "rgb(255, 99, 132)",
                "rgb(54, 162, 235)",
                "rgb(255, 205, 86)",
                "rgb(75, 192, 192)",
              ],
              data: data.data,
            },
          ],
        },
        options: {},
      });
    },
  },
};
</script>
