<template>
    <div class="row">
        <div class="col-12 text-center py-3">
            <button @click="shutdownRestaurant()" v-if="restaurant?.creator.id == account.id" class="btn btn-outline-danger">SHUT DOWN</button>
<h1 class="text-danger pt-2">
    {{restaurant?.name}}
</h1>
<span v-for="r in restaurant?.exposure" :key="r">🐀</span>
        </div>
            <div class="col-12">
                <h2 class="text-center py-3">REPORTS:</h2>
                <div class="row">
<div class="col-12 py-3">
    <div class="bg-dark p-3 rounded">
        <h3>Generate Report</h3>
        <form @submit.prevent="createReport()">
         <input type="text" name="title" placeholder="Title" class="px-3" v-model="editable.title">
         <input type="range" max="5" name="rating" v-model="editable.rating">
         <input class="w-100" name="body" type="textarea" v-model="editable.body">
         <button type="submit">Submit</button>
        </form>
    </div>
</div>
                </div>
            </div>
        <div class="col-12" v-for="r in reports" :key="r.id">
            <ReportCard :report="r"/>
        </div>
    </div>
</template>
<script>
import { computed, ref } from '@vue/reactivity';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import { restaurantsService } from '../services/RestaurantsService';
import { reportsService} from '../services/ReportsService'
import { useRoute, useRouter } from 'vue-router';
import { onMounted } from '@vue/runtime-core';
import ReportCard from '../components/ReportCard.vue';
import Pop from '../utils/Pop';
export default {
    setup() {
        const editable = ref({})
        const route = useRoute();
        const router = useRouter();
        async function getActiveRestaurant() {
            try {
                if (!AppState.activeRestaurant) {
                    await restaurantsService.getOne(route.params.id);
                }
            }
            catch (error) {
                logger.log(error);
                router.push({name: 'Home'})
            }
        }
        async function getReports() {
            try {
                await reportsService.getReports(route.params.id);
            }
            catch (error) {
                logger.error(error);
            }
        }
        onMounted(() => {
            getActiveRestaurant();
            getReports();
        });
        return {
            route,
            editable,
            async createReport() {
                try {
        editable.value.restaurantId = route.params.id
        await reportsService.createReport(editable.value)
    } catch (error) {
        logger.error(error)
    }
            },
            async shutdownRestaurant() {
                try {
                    AppState.activeRestaurant.shutdown = !AppState.activeRestaurant.shutdown
                    await restaurantsService.shutdownRestaurant(AppState.activeRestaurant)
                    Pop.toast(`<img class="img-fluid" src="https://gifimage.net/wp-content/uploads/2018/11/gordon-ramsay-shut-it-down-gif-4.gif"/>`)
    } catch (error) {
        logger.error(error)
    }
},
            restaurant: computed(() => AppState.activeRestaurant),
            reports: computed(() => AppState.reports),
            account: computed(()=>AppState.account)
        };
    },
    components: { ReportCard }
};
</script>
<style ></style>