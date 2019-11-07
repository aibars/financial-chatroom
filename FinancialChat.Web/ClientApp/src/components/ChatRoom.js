import * as React from 'react';
import { connect } from 'react-redux';
import * as signalR from '@aspnet/signalr';
import { Link } from 'react-router-dom';
import '../styles/ChatRoom.css';

class ChatRoom extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      message: '',
      messages: [],
      hubConnection: null,
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

    this.setState({ hubConnection: connection }, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log('Connection started.'))
        .catch(err => console.log('Error while establishing connection.'));

      this.state.hubConnection.on('sendToAll', (username, receivedMessage) => {
        const text = `${username}: ${receivedMessage}`;
        const messages =
          this.state.messages.length > 49 ?
            this.state.messages.slice(this.state.messages.length - 49, this.state.messages.length).concat([text])
            : this.state.messages.concat([text]);
        
        console.log(messages.length);
        this.setState({ messages });
      });

      this.state.hubConnection.on('sendToAllFromBot', (receivedMessage) => {
        const text = `FinancialBot: ${receivedMessage}`;
        const messages = this.state.messages.concat([text]);
        this.setState({ messages });
      });
    });
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
        <br />
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
        <div>
          {this.state.messages.map((message, index) => (
            <span style={{ display: 'block' }} key={index}>{index + 1}. {message}</span>
          ))}
        </div>
        
      </div>
    );
  }
}

const connectedRoom = connect((state) => {
  const { authentication } = state;
  const { user } = authentication;
  return { user };
})(ChatRoom);
export { connectedRoom as ChatRoom };
