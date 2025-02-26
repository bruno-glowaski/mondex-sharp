<template>
  <form @submit="onSubmit">
    <v-card :title="title">
      <v-card-text>
        <v-text-field
          label="Number"
          :error-messages="errors.number"
          v-bind="numberProps"
          v-model="number"
        />
      </v-card-text>
      <v-text-field
        label="Name"
        :error-messages="errors.name"
        v-bind="nameProps"
        v-model="name"
      />
      <v-text-field
        label="Description"
        :error-messages="errors.description"
        v-bind="descriptionProps"
        v-model="description"
      />

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn type="submit" color="success" variant="flat"> Create </v-btn>
        <v-btn @click="emit('close')">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </form>
</template>

<script lang="ts">
export const UpsertPokemonSpeciesSchema = z.object({
  number: z.number().nonnegative(),
  name: z.string().trim().nonempty(),
  genera: z.string().trim().nonempty(),
  description: z.string().trim(),
  types: z.array(z.number().int()).min(1).max(2),
  base_stats: z.object({
    hp: z.number().int().positive(),
    attack: z.number().int().positive(),
    defense: z.number().int().positive(),
    special_attack: z.number().int().positive(),
    special_defense: z.number().int().positive(),
    speed: z.number().int().positive(),
  }),
});
export type UpsertPokemonSpecies = z.infer<typeof UpsertPokemonSpeciesSchema>;
</script>

<script setup lang="ts">
import { z } from "zod";

defineProps<{ title: string }>();
const emit = defineEmits<{
  (e: "submit", data: UpsertPokemonSpecies): Promise<void>;
  (e: "close"): void;
}>();

const { handleSubmit, defineField, errors } = useForm({
  validationSchema: toTypedSchema(UpsertPokemonSpeciesSchema),
});

const [number, numberProps] = defineField("number");
const [name, nameProps] = defineField("name");
const [genera, generaProps] = defineField("genera");
const [description, descriptionProps] = defineField("description");
const [types, typesProps] = defineField("types");
const [hp, hpProps] = defineField("base_stats.hp");
const [attack, attackProps] = defineField("base_stats.attack");
const [defense, defenseProps] = defineField("base_stats.defense");
const [specialAttack, specialAttackProps] = defineField(
  "base_stats.special_attack",
);
const [specialDefense, specialDefenseProps] = defineField(
  "base_stats.special_defense",
);
const [speed, speedProps] = defineField("base_stats.speed");

const onSubmit = handleSubmit(async (values) => {
  await emit("submit", values);
  emit("close");
});
</script>
