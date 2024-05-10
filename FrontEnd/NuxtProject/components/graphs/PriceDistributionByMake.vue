<template>
  <canvas id="priceDistributionChart"></canvas>
</template>

<script>
import axios from "axios";
import Chart from "chart.js/auto";
import { priceByMakeEndPoint } from "~/constants/endpoints";

export default {
  mounted() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const response = await axios.get(priceByMakeEndPoint);
        const priceDistributionByMakeData = {
          labels: Object.keys(response.data),
          data: Object.values(response.data),
        };
        console.log(
          "Price Distribution By Make Data:",
          priceDistributionByMakeData
        );
        this.createChart(priceDistributionByMakeData);
      } catch (error) {
        console.error("Error fetching price distribution by make data:", error);
      }
    },
    createChart(data) {
      const ctx = document
        .getElementById("priceDistributionChart")
        .getContext("2d");
      new Chart(ctx, {
        type: "scatter",
        data: {
          datasets: [
            {
              label: "Price by make",
              backgroundColor: "#f96182",
              borderColor: "#f96182",
              data: data.labels.map((label, index) => ({
                x: label,
                y: data.data[index],
              })),
            },
          ],
        },
        options: {
          scales: {
            x: {
              type: "category",
              position: "bottom",
            },
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    },
  },
};
</script>
