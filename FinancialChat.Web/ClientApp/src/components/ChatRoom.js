import * as React from 'react';
import { connect } from 'react-redux';
import * as signalR from '@aspnet/signalr';
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
  }

  componentDidMount = () => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`/chathub?token=${JSON.parse(localStorage.getItem('user')).token}`)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.setState({ hubConnection: connection }, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log('Connection started.'))
        .catch(err => console.log('Error while establishing connection.'));

      this.state.hubConnection.on('sendToAll', (username, receivedMessage) => {
        const text = `${username}: ${receivedMessage}`;
        const messages = this.state.messages.concat([text]);
        this.setState({ messages });
      });
    });
  }

  render() {
    return (
      <div className="chat-input-box">
        <br />
        <input className="login-input"
          type="text"
          value={this.state.message}
          onChange={e => this.setState({ message: e.target.value })}
        />

        <button className="login-btn chat-send-btn" onClick={this.sendMessage}>Send</button>

        <div>
          {this.state.messages.map((message, index) => (
            <span style={{ display: 'block' }} key={index}> {message} </span>
          ))}
        </div>
      </div>
    );
  }
}

const connectedRoom = connect()(ChatRoom);
export { connectedRoom as ChatRoom };
