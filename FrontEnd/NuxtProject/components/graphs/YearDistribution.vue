<template>
  <canvas id="yearDistributionChart"></canvas>
</template>

<script>
import axios from "axios";
import Chart from "chart.js/auto";
import { yearDistributionEndPoint } from "~/constants/endpoints";

export default {
  mounted() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const response = await axios.get(yearDistributionEndPoint);
        const yearDistributionData = {
          labels: Object.keys(response.data),
          data: Object.values(response.data),
        };
        console.log("Year Distribution Data:", yearDistributionData);
        this.createChart(yearDistributionData);
      } catch (error) {
        console.error("Error fetching year distribution data:", error);
      }
    },
    createChart(data) {
      const ctx = document
        .getElementById("yearDistributionChart")
        .getContext("2d");
      new Chart(ctx, {
        type: "bar",
        data: {
          labels: data.labels,
          datasets: [
            {
              label: "Distribución de años de los coches",
              backgroundColor: "#f96182",
              borderColor: "#f96182",
              data: data.data,
            },
          ],
        },
        options: {
          scales: {
            x: {
              title: {
                display: true,
                text: "Años de los coches",
              },
            },
            y: {
              beginAtZero: true,
              title: {
                display: true,
                text: "Cantidad de coches",
              },
            },
          },
        },
      });
    },
  },
};
</script>
