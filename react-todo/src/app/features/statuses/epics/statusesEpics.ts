import { Epic, ofType } from "redux-observable";
import { GET_STATUSES } from "../actions/actionTypes";
import { catchError, from, map, of, switchMap } from "rxjs";
import { ajax } from "rxjs/ajax";
import { createRequest } from "../../../../misc/RequestCreator";
import { RootAction, Status } from "../../../../types";
import { getStatusesFailure, getStatusesSuccess } from "../actions/statusActions";
import { getStatusesQuery } from "../../../../ApiQueries/statusesQueries";

export const getStatusesEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(GET_STATUSES),
        switchMap((action) =>
            from(
                ajax(createRequest(getStatusesQuery(), action.payload))
                    .pipe(
                        map((response: any) => getStatusesSuccess((response.response.data.statuses as Status[]))),
                        catchError(error => of(getStatusesFailure(error)))
                    )
            )
        )
    );