<template>
  <div class="row">
    <div class="col-md-12 my-3" v-for="r in restaurants" :key="r.id">
     <RestaurantCard :restaurant="r"/>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core';
import { logger } from '../utils/Logger';
import { restaurantsService} from "../services/RestaurantsService"
import { AppState } from '../AppState';
import RestaurantCard from '../components/RestaurantCard.vue';

export default {
    name: "Home",
    setup() {
        async function getRestaurants() {
            try {
                await restaurantsService.getRestaurants();
            }
            catch (error) {
                logger.log(error);
            }
        }
        onMounted(() => {
            getRestaurants();
        });
        return {
            restaurants: computed(() => AppState.restaurants)
        };
    },
    components: { RestaurantCard }
}
</script>

<style scoped lang="scss">
.home{
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
