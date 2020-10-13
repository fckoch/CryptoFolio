import React, { Component } from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import AuthService from '../../services/authenticationService.js'
import { withStyles } from '@material-ui/core/styles';
import { Redirect } from 'react-router-dom'

const styles = theme => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
});

class SignIn extends Component {
  constructor(props) {
    super()
    this.state = {
      email: '',
      emailError: false,
      emailErrorMessage: '',
      password: '',
      passwordError: false,
      PasswordErrorMessage: '',
      boxcsstype: 'result-box-none',
      formMessage: '',
      redirect: ''
    }
  }

  onChangeEmail = (e) => {
    this.setState({
      email: e.target.value
    })
  }

  onChangePassword = (e) => {
    this.setState({
      password: e.target.value
    })
  }

  validate = () => {
    let isError = false;
    if (this.state.email.length === 0) {
      isError = true
      this.setState({
        emailError: true,
        emailErrorMessage: 'Please enter an e-mail'
      })
    }

    if (this.state.password.length === 0) {
      isError = true
      this.setState({
        passwordError: true,
        PasswordErrorMessage: 'Please enter a password'
      })
    }

    return isError;
  }

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Redirect to='/wallet' />
    }
  }

  onSubmit = (e) => {
    e.preventDefault();

    //Clear errors
    this.setState({
      emailError: false,
      emailErrorMessage: '',
      passwordError: false,
      PasswordErrorMessage: '',
      formMessage: '',
      boxcsstype: 'result-box-none',
    })

    const err = this.validate();

    if (!err) {

      (async () => {
        try {
          const response = await AuthService.login(this.state.email, this.state.password);
          if (response.status >=200 & response.status < 300) {
            localStorage.setItem('user', JSON.stringify(response.data.token).replace(/\"/g,''));
            //Clear form
            this.setState({
              email: '',
              password: '',
              formMessage: '',
              boxcsstype: 'result-box-none',
              redirect: true
            })
          this.props.onUserChange('signin');;
          }
        } 
        catch (error) {
          if (error.response.status === 400) {
            this.setState({
              formMessage: error.response.data,
              boxcsstype: 'result-box-failure'
            })
          }
          else {
            console.log('There was an error!', error)
          }
        }
      })();
    }
  }
    
  render() {

    const { classes } = this.props;

    return (
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <form className={classes.form} noValidate>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              autoFocus
              onChange={this.onChangeEmail}
              value={this.state.email}
              error={this.state.emailError}
              helperText={this.state.emailErrorMessage}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
              onChange={this.onChangePassword}
              value={this.state.password}
              error={this.state.passwordError}
              helperText={this.state.PasswordErrorMessage}
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
              onClick={this.onSubmit}
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item>
                <Link href="/register" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
                {<p className={this.state.boxcsstype}> {this.state.formMessage}</p>}
                {this.renderRedirect()}
              </Grid>
            </Grid>
          </form>
        </div>
      </Container>
    );
  }
}

export default withStyles(styles)(SignIn);