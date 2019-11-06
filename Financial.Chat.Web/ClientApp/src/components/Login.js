import React from 'react';
import '../styles/Login.css';
import { connect } from 'react-redux';
import { login } from '../actionCreators';

class Login extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            submitted: false
        };

        this.submitLogin = this.submitLogin.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    submitLogin(e) {
        e.preventDefault();
        this.setState({ submitted: true });
        const { username, password } = this.state;
        if (username && password) {
            this.props.login(username, password);
        }
    }

    handleChange(e) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    render() {
        return (
            <div className="box-container">
                <div className="inner-container">
                    <div className="header">
                        Login
                    </div>
                    <div className="box">
                        <div className="input-group">
                            <label htmlFor="username">Username</label>
                            <input
                                type="text"
                                name="username"
                                className="login-input"
                                placeholder="Username"
                                value={this.state.username}
                                onChange={this.handleChange} />
                        </div>
                        <div className="input-group">
                            <label htmlFor="password">Password</label>
                            <input
                                type="password"
                                name="password"
                                className="login-input"
                                placeholder="Password"
                                value={this.state.password}
                                onChange={this.handleChange} />
                        </div>
                        <button
                            type="button"
                            className="login-btn"
                            onClick={this.submitLogin}>Login</button>
                    </div>
                </div>
            </div>
        );
    }

}

const mapStateToProps = state => (state);

const actionCreators = {
    login: login,
};

const connectedLoginPage = connect(mapStateToProps, actionCreators)(Login);
export { connectedLoginPage as Login };