let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {
  loggedIn: true,
  user
} : {};

const constants = {
  LOGIN_REQUEST: 'LOGIN_REQUEST',
  LOGIN_SUCCESS: 'LOGIN_SUCCESS',
  LOGIN_FAILURE: 'LOGIN_FAILURE',
  GET_MESSAGES_SUCCESS: 'GET_MESSAGES_SUCCESS',
  GET_MESSAGES_FAILURE: 'GET_MESSAGES_FAILURE',
  GET_MESSAGES_REQUEST: 'GET_MESSAGES_REQUEST'
};

export function authentication(state = initialState, action) {
  switch (action.type) {
    case constants.LOGIN_REQUEST:
      return {
        loggingIn: true,
        user: action.user
      };
    case constants.LOGIN_SUCCESS:
      return {
        loggedIn: true,
        user: action.user
      };
    case constants.LOGIN_FAILURE:
      return {};
    default:
      return state
  }
}

export function messages(state = { items: [] }, action) {
  switch (action.type) {
    case constants.GET_MESSAGES_REQUEST:
      return { items: [] };
    case constants.GET_MESSAGES_SUCCESS:
      return {
        items: action.messages,
      };
    case constants.GET_MESSAGES_FAILURE:
      return { items: [] };
    default:
      return state;
  }
}