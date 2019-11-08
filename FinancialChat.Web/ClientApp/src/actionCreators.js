import { service } from './services';
import { history } from './history';

const constants = {
    LOGIN_REQUEST: 'LOGIN_REQUEST',
    LOGIN_SUCCESS: 'LOGIN_SUCCESS',
    LOGIN_FAILURE: 'LOGIN_FAILURE',
    GET_MESSAGES_REQUEST: 'GET_MESSAGES_REQUEST',
    GET_MESSAGES_SUCCESS: 'GET_MESSAGES_SUCCESS',
    GET_MESSAGES_FAILURE: 'GET_MESSAGES_FAILURE',
};

export function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        service.login(username, password)
            .then(
                user => {
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error.toString()));
                }
            );

        function request(user) {
            return { type: constants.LOGIN_REQUEST, user }
        }

        function success(user) {
            return { type: constants.LOGIN_SUCCESS, user }
        }

        function failure(error) {
            return { type: constants.LOGIN_FAILURE, error }
        }
    };
}

export function getMessages() {
    return dispatch => {
        dispatch(request());

        service.getMessages()
            .then(
                messages => dispatch(success(messages)),
                error => dispatch(failure(error.toString()))
            );
    };

    function request() {
        return { type: constants.GET_MESSAGES_REQUEST }
    }

    function success(messages) {
        return { type: constants.GET_MESSAGES_SUCCESS, messages }
    }

    function failure(error) {
        return { type: constants.GET_MESSAGES_FAILURE, error }
    }

}