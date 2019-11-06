import * as React from 'react';
import { Router, Route, Redirect, Switch } from 'react-router';
import Home from './Home';
import PrivateRoute from 'react-private-route';
import { Login } from './Login';
import { history } from '../history';
import '../styles/custom.css'



class App extends React.Component {
    isLoggedIn() {
        return localStorage.user ? true : false;
    }

    render() {
        return (
            <Router history={history}>
                <Switch>
                    <PrivateRoute exact path="/" component={Home}
                        isAuthenticated={!!this.isLoggedIn()} />
                    <Route path="/login" component={Login} />
                    <Redirect from="*" to="/" />
                </Switch>
            </Router>
        );
    }

}

export default App;