import { Epic, ofType } from "redux-observable";
import { GET_CATEGORIES } from "../actions/actionTypes";
import { catchError, from, map, of, switchMap } from "rxjs";
import { ajax } from "rxjs/ajax";
import { createRequest } from "../../../../misc/RequestCreator";
import { getCategoriesQuery } from "../../../../ApiQueries/categoryQueries";
import { Category, RootAction } from "../../../../types";
import { getCategoriesFailure, getCategoriesSuccess } from "../actions/categoryActions";

export const getCategoriesEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(GET_CATEGORIES),
        switchMap((action) =>
            from(
                ajax(createRequest(getCategoriesQuery(), action.payload))
                    .pipe(
                        map((response: any) => getCategoriesSuccess((response.response.data.categories as Category[]))),
                        catchError(error => of(getCategoriesFailure(error)))
                    )
            )
        )
    );