<template>
  <v-container>
    <v-card>
      <v-card-title>Categorias</v-card-title>
      <v-card-text
        ><v-btn
          class="my-3"
          rounded="xs"
          size="large"
          color="green"
          variant="flat"
          to="/categorias/criar"
          block
          >Adicionar categoria</v-btn
        >
        <v-table>
          <thead>
            <tr class="font-weight-bold">
              <th class="text-left font-weight-bold">ID</th>
              <th class="text-left font-weight-bold">Nome</th>
              <th class="text-left font-weight-bold">Descrição</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="categoria in categorias" :key="categoria.id">
              <td>{{ categoria.id }}</td>
              <td>{{ categoria.nome }}</td>
              <td>{{ categoria.descricao }}</td>
              <td class="text-center">
                <v-btn
                  @click="redirectToEdit(categoria.id)"
                  class="mr-3"
                  density="comfortable"
                  icon="mdi-pencil"
                  variant="elevated"
                ></v-btn>
                <v-btn
                  density="comfortable"
                  icon="mdi-delete"
                  color="red"
                  @click="openDeleteDialog(categoria.id)"
                ></v-btn>
              </td>
            </tr>
          </tbody>
        </v-table>
        <v-progress-linear
          :active="loading"
          color="green"
          indeterminate
        ></v-progress-linear>
      </v-card-text>
    </v-card>

    <!-- Diálogo para confirmação do botão de excluir -->
    <v-dialog v-model="deleteDialog" max-width="500px">
      <v-card>
        <v-card-title class="headline">Confirme se quer excluir</v-card-title>
        <v-card-text
          >Você tem certeza de que deseja excluir essa categoria?</v-card-text
        >
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue" variant="flat" text @click="deleteDialog = false"
            >Cancelar</v-btn
          >
          <v-btn
            color="red darken-1"
            variant="outlined"
            text
            @click="deleteCategoria"
            >Excluir</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

const categorias = ref([]);
const deleteDialog = ref(false);
const categoriaIdToDelete = ref(null);
const router = useRouter();
const loading = ref(true);

const redirectToEdit = (cId) => {
  router.push({ name: "EditarCategoria", params: { id: cId } });
};

const fetchCategorias = async () => {
  try {
    loading.value = true;
    const response = await axios.get("https://localhost:7118/api/categorias");
    categorias.value = response.data;
  } catch (error) {
    console.error("Ocorreu um erro ao buscar as categorias", error);
  } finally {
    loading.value = false;
  }
};

const openDeleteDialog = (categoriaId) => {
  categoriaIdToDelete.value = categoriaId;
  deleteDialog.value = true;
};

const deleteCategoria = async () => {
  try {
    await axios.delete(
      `https://localhost:7118/api/categorias/${categoriaIdToDelete.value}`
    );
    deleteDialog.value = false;
    fetchCategorias();
    produtos.value = products.value.filter(
      (product) => product.id !== produtoIdToDelete.value
    );
  } catch (error) {
    console.error("Error deleting product:", error);
  }
};

onMounted(fetchCategorias);
</script>
