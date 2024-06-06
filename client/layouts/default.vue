<template>
  <v-app>
    <v-app-bar app>
      <v-toolbar-title>ZDZCode</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn text to="/">Produtos</v-btn>
      <v-btn text to="/categorias">Categorias</v-btn>
    </v-app-bar>
    <v-main>
      <NuxtPage />
    </v-main>
  </v-app>
</template>

<script setup></script>
