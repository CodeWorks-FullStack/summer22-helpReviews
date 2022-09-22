import { logger } from "../utils/Logger"
import { api } from '../services/AxiosService'
import { AppState } from "../AppState"
class ReportsService {
    async getReports(id) {
        const res = await api.get(`api/restaurants/${id}/reports`)
        logger.log('reports', res.data)
        AppState.reports = res.data
    }

    async createReport(reportData) {
        const res = await api.post('api/reports', reportData)
        logger.log(res.data, 'new report')
        AppState.reports.push(res.data)
    }
}

export const reportsService = new ReportsService()