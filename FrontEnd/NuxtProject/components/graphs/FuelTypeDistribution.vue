<template>
  <canvas id="fuelTypeDistributionChart"></canvas>
</template>

<script>
import axios from "axios";
import Chart from "chart.js/auto";
import { fuelTypeEndPoint } from "~/constants/endpoints";

export default {
  mounted() {
    this.fetchFuelTypeDistribution();
  },
  methods: {
    async fetchFuelTypeDistribution() {
      try {
        const response = await axios.get(fuelTypeEndPoint);
        const fuelTypeDistributionData = response.data;
        console.log("Fuel Type Distribution Data:", fuelTypeDistributionData);
        this.createChart(fuelTypeDistributionData);
      } catch (error) {
        console.error("Error fetching fuel type distribution data:", error);
      }
    },
    createChart(data) {
      const ctx = document
        .getElementById("fuelTypeDistributionChart")
        .getContext("2d");
      new Chart(ctx, {
        type: "pie", // Pie chart type
        data: {
          labels: Object.keys(data),
          datasets: [
            {
              label: "Fuel Type Distribution",
              data: Object.values(data),
              backgroundColor: [
                "rgba(255, 99, 132, 0.75)",
                "rgba(54, 162, 235, 0.75)",
                "rgba(255, 206, 86, 0.75)",
                "rgba(75, 192, 192, 0.75)",
                "rgba(153, 102, 255, 0.75)",
                "rgba(255, 159, 64, 07.5)",
              ],
              borderColor: [
                "rgba(255, 99, 132)",
                "rgba(54, 162, 235)",
                "rgba(255, 206, 86)",
                "rgba(75, 192, 192)",
                "rgba(153, 102, 255)",
                "rgba(255, 159, 64)",
              ],
              borderWidth: 1,
            },
          ],
        },
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: "top",
            },
            title: {
              display: true,
              text: "Fuel Type Distribution",
            },
          },
        },
      });
    },
  },
};
</script>
