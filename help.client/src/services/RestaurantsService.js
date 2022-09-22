import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RestaurantsService {

    async getRestaurants() {
        const res = await api.get('api/restaurants')
        // logger.log(res.data, 'getting restaurants')
        AppState.restaurants = res.data
        // logger.log('appstate', AppState.restaurants)
    }


    async getOne(id) {
        const res = await api.get(`api/restaurants/${id}`)
        logger.log('get one', res.data)
        AppState.activeRestaurant = res.data
        logger.log('active', AppState.activeRestaurant)
    }

    async shutdownRestaurant(restaurant) {
        const res = await api.put(`api/restaurants/${restaurant.id}`, restaurant)
        logger.log('edited', res.data)
    }
}

export const restaurantsService = new RestaurantsService()