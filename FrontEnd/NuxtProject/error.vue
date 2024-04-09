<template>
    <div class="error-page">
      <div class="container">
        <div class="content">
          <h1>404</h1>
          <h2>Página no encontrada</h2>
          <p>Lo sentimos, la página que estás buscando no existe.</p>
          <router-link to="/">Ir a la página de inicio</router-link>
        </div>
      </div>
    </div>
  </template>
  
  <style scoped lang="scss">
  .error-page {
    background-color: #f8f9fa;
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100vh;
  }
  
  .container {
    max-width: 600px;
    text-align: center;
  }
  
  .content {
    padding: 40px;
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
  }
  
  h1 {
    font-size: 5rem;
    color: #dc3545;
    margin-bottom: 10px;
  }
  
  h2 {
    font-size: 2rem;
    margin-bottom: 20px;
  }
  
  p {
    font-size: 1.2rem;
    margin-bottom: 20px;
  }
  
  router-link {
    font-size: 1.2rem;
    color: #007bff;
    text-decoration: underline;
    cursor: pointer;
  }
  </style>
  