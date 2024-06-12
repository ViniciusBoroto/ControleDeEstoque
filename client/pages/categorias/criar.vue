<template>
  <v-container>
    <v-btn
      to="/categorias"
      class="my-3"
      color="blue"
      variant="outlined"
      prepend-icon="mdi-arrow-left"
    >
      <template v-slot:prepend>
        <v-icon color="blue"></v-icon>
      </template>

      Voltar
    </v-btn>
    <v-card>
      <v-card-title>Criar categoria</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="handleSubmit">
          <v-text-field
            v-model="formData.nome"
            label="Nome"
            required
          ></v-text-field>
          <v-text-field
            v-model="formData.descricao"
            label="Descrição"
            required
          ></v-text-field>
          <v-btn type="submit" color="primary">Criar</v-btn>
        </v-form>
        <v-alert
          class="my-5"
          v-if="successMessage"
          type="success"
          dismissible
          @click="successMessage = ''"
        >
          {{ successMessage }}
        </v-alert>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>
import { ref } from "vue";
import axios from "axios";

const formData = ref({
  nome: "",
  descricao: "",
});

const { id } = useRoute().params;

const successMessage = ref("");

const handleSubmit = async () => {
  try {
    const response = await axios.post(
      "https://localhost:7118/api/categorias",
      formData.value
    );
    successMessage.value = "Categoria criada com sucesso!";
    formData.value = { nome: "", descricao: "" };
  } catch (error) {
    console.error("Erro durante a criação da categoria", error);
  }
};
</script>
