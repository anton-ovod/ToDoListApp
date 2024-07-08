import { Category } from '../../../../types';
import { GET_CATEGORIES_FAILURE, GET_CATEGORIES_SUCCESS } from "../actions/actionTypes";

const categoriesReducer = (state: Category[] = [], action: any) : Category[] => {
    switch (action.type) {
        case GET_CATEGORIES_SUCCESS:
            console.log("Categories fetched sucessfully!");
            return action.payload;
        case GET_CATEGORIES_FAILURE:
            console.log("Error while fetching categories: ", action.payload);
            return state;
        default:
            return state;
    }
};

export default categoriesReducer;