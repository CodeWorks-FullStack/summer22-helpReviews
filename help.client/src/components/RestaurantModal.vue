<template>
    <div class="modal fade" id="restaurantModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header ">
                    <span v-for="r in restaurant?.exposure" :key="r">🐀</span>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body ">
                    <div class="row">
                    <div class="col-6">
                        <img class="img-fluid" :src="restaurant?.imgUrl" alt="">
                    </div>
                    <div class="col-6">
                        <div class="text-center">
                            <div class="pt-2">
                                <h1 class="border-bottom ">{{restaurant?.name}}</h1>
                            </div>
                            <div class="pt-2">
                                {{restaurant?.description}}
                                <p>Reports: {{restaurant?.reportCount}}</p>
                            </div>
                        </div>
                    </div>
           
                </div>
                </div>
                <div class="modal-footer">
                
                    <router-link v-if="restaurant" :to="{name: 'Restaurant Details', params: {id:restaurant?.id}}">
                        <button type="button" data-bs-dismiss="modal" class="btn btn-primary">See Details</button>
                    </router-link>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { computed } from '@vue/runtime-core';
import { AppState } from '../AppState';
export default {
setup() {
        return {
restaurant: computed(()=>AppState.activeRestaurant)
  };
},
};
</script>
<style>
    /* .modal-img{
        height: 650px;
        width: 600px;
        object-fit: cover;
    } */

</style>