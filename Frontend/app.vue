<template>
  <v-app>
    <v-layout>
      <v-navigation-drawer class="pt-4" expand-on-hover rail>
        <v-list nav density="compact">
          <v-list-item
            title="Home"
            to="/"
            prepend-icon="mdi-home"
            color="primary"
          />
          <v-list-group value="Pokémon">
            <template v-slot:activator="{ props }">
              <v-list-item
                v-bind="props"
                title="Pokémon"
                prepend-icon="mdi-pokemon-go"
                color="primary"
              />
            </template>
            <v-list-item
              title="Types"
              to="/types"
              color="primary"
              prepend-icon="mdi-cube"
            />
            <v-list-item
              title="Species"
              to="/species"
              color="primary"
              prepend-icon="mdi-cube"
            />
          </v-list-group>
        </v-list>
      </v-navigation-drawer>
      <v-main>
        <nuxt-page />
      </v-main>
    </v-layout>
  </v-app>
</template>

<script setup lang="ts">
useHead({
  titleTemplate: (titleChunk) => {
    return titleChunk ? `${titleChunk} - MonDex Sharp` : "MonDex Sharp";
  },
});
</script>
