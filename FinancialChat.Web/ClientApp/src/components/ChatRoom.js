import * as React from 'react';
import { connect } from 'react-redux';
import * as signalR from '@aspnet/signalr';
import { Link } from 'react-router-dom';
import '../styles/ChatRoom.css';
import { service } from '../services';
import moment from 'moment';

class ChatRoom extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      message: '',
      hubConnection: null,
      messages: [],
    };

    this.sendMessage = this.sendMessage.bind(this);
    this.handleKeyPress = this.handleKeyPress.bind(this);
  }

  componentDidMount = () => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl('/chathub', {
        accessTokenFactory: () => this.props.user.token
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    service.getMessages()
      .then(
        messages => this.setState({ messages: messages })
      );

    this.setState({ hubConnection: connection }, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log('Connection started.'))
        .catch(() => console.log('Error while establishing connection.'));

      this.state.hubConnection.on('sendToAll', (username, receivedMessage) => {
        var items = this.state.messages;
        const messages =
          items.length > 49 ?
            items.slice(items.length - 49, items.length).concat([{ text: receivedMessage, userName: username, sendDate: this.getCurrentTime() }])
            : items.concat([{ text: receivedMessage, userName: username, sendDate: this.getCurrentTime() }]);
        this.setState({ messages: messages });
      });

      this.state.hubConnection.on('sendToAllFromBot', (receivedMessage) => {
        var items = this.state.messages;
        const messages =
          items.length > 49 ?
            items.slice(items.length - 49, items.length).concat([{ text: receivedMessage, userName: 'FinancialBot', sendDate: this.getCurrentTime() }])
            : items.concat([{ text: receivedMessage, userName: 'FinancialBot', sendDate: this.getCurrentTime() }]);
        this.setState({ messages: messages });
      });
    });
  }

  getCurrentTime() {
    return new Date().toISOString();
  }

  handleKeyPress = (e) => {
    if (e.charCode === 13) {
      this.sendMessage();
    }
  }

  sendMessage = () => {
    if (this.state.message === '') return;
    this.state.hubConnection
      .invoke('send', this.state.message)
      .catch(err => console.error(err));

    this.setState({ message: '' });
  };

  render() {
    return (
      <div className="chat-input-box">
        <div id="status-line">
          <input className="login-input"
            type="text"
            onKeyPress={this.handleKeyPress}
            value={this.state.message}
            onChange={e => this.setState({ message: e.target.value })}
          />

          <button className="login-btn chat-send-btn" onClick={this.sendMessage}>Send</button>
          <span className="login-label">Logged in as: <label>{this.props.user.userName}</label>
            <Link onClick={() => localStorage.removeItem('user')} className="logout-label" to="/login">Logout</Link>
          </span>
        </div>
        <div>
          {this.state.messages.map((item, index) => (
            <span className={"chat-line-" + (index % 2 === 0 ? "even" : "odd")}
              key={index}>{item.userName}: {item.text}  <label className="send-date">{moment(item.sendDate).format('YYYY/DD/MM hh:mm')}</label>
            </span>
          ))}
        </div>
      </div>
    );
  }
}

const connectedRoom = connect((state) => {
  const { authentication, messages } = state;
  const { user } = authentication;
  return { user, messages };
})(ChatRoom);

export { connectedRoom as ChatRoom };
