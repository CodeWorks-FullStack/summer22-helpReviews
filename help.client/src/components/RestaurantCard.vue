<template>
<div class="rounded elevation-3 d-flex justify-content-between selectable" @click="setActive()">
    <img class="restaurant-img" :src="restaurant.imgUrl" alt="">
    <div class="p-3">
    <h1 class="border-bottom border-danger">{{restaurant.name}}</h1>
    </div>
</div>
<RestaurantModal/>

</template>
<script>
import { Modal } from 'bootstrap';
import { logger } from '../utils/Logger'
import RestaurantModal from './RestaurantModal.vue'
import { restaurantsService } from '../services/RestaurantsService';
export default {
    props: {
        restaurant: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        return {
            async setActive() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("restaurantModal")).toggle();
                    await restaurantsService.getOne(props.restaurant.id)
                }
                catch (error) {
                    logger.error(error);
                }
            }
        };
    },
    components: { RestaurantModal }
};
</script>
<style> 
.restaurant-img{
    height: 200px;
    width: 250px;
    object-fit: cover;
}

</style>