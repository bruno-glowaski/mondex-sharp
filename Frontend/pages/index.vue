<template>
  <v-container class="pa-8" width="md" height="100%">
    <v-card class="pa-4 self-center">
      <v-row align="stretch">
        <v-col>
          <v-btn block size="large" color="primary" to="/types"
            >Pokémon Types</v-btn
          >
        </v-col>
      </v-row>
      <v-row align="stretch">
        <v-col>
          <v-btn block size="large" color="primary" disabled
            >Pokémon Species</v-btn
          >
        </v-col>
      </v-row>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
useHead({ title: "Home" });
</script>
