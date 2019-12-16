import React from 'react';
import renderer from 'react-test-renderer';
import App from '../components/App';
import { ConnectedRouter } from 'connected-react-router';
import { history } from '../history';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';

const mockStore = configureStore([]);

describe('App', () => {
  let store;
  let component;

  beforeEach(() => {
    store = mockStore({

    });

    store.dispatch = jest.fn();

    component = renderer.create(
      <Provider store={store}>
        <ConnectedRouter history={history}>
          <App />
        </ConnectedRouter>
      </Provider>
    );
  });

  it('should render without crashing', () => {
    expect(component.toJSON()).toMatchSnapshot();
  });
});