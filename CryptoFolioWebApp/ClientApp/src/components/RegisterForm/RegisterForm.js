import React, { Component } from 'react';
import { Redirect } from 'react-router-dom'
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
import './RegisterForm.css';

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
    marginTop: theme.spacing(3),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
});

class SignUp extends Component {
  constructor(props) {
    super()
    this.state = {
      firstName: '',
      firstNameError: false,
      firstNameErrorMessage: '',
      lastName: '',
      lastNameError: false,
      lastNameErrorMessage: '',
      email: '',
      emailError: false,
      emailErrorMessage: '',
      password: '',
      passwordError: false,
      passwordErrorMessage: '',
      formMessage: '',
      succesful: false,
      cssboxtype: 'result-box-none',
      redirect: ''
    }
  }
  
  onChangeFirstName = (e) => {
    this.setState({
      firstName: e.target.value
    });
  }

  onChangeLastName = (e) => {
    this.setState({
      lastName: e.target.value
    });
  }

  onChangeEmail = (e) => {
    this.setState({
      email: e.target.value
    });
  }

  onChangePassword = (e) => {
    this.setState({
      password: e.target.value
    });
  }

  validate = () => {
    let isError = false;
    if (this.state.firstName.length < 2 || (/[^a-z]/i.test(this.state.firstName))) {
      isError = true
      this.setState({
        firstNameError: true,
        firstNameErrorMessage: 'Please enter a valid name',
      })
    }

    if (this.state.lastName.length < 2 || (/[^a-z]/i.test(this.state.lastName))) {
      isError = true
      this.setState({
        lastNameError: true,
        lastNameErrorMessage: 'Please enter a valid last name',
      })
    }

    if (this.state.password.length < 7 || this.state.password.length > 40) {
      isError = true
      this.setState({
        passwordError: true,
        passwordErrorMessage: 'Password needs to be between 7 and 40 characters',
      })
    }

    if (this.state.email.indexOf("@") === -1) {
      isError = true
      this.setState({
        emailError: true,
        emailErrorMessage: 'Please enter a valid e-mail',
      })
    }
    return isError;
  }

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Redirect to='/login' />
    }
  }

  onSubmit = (e) => {
    e.preventDefault();

    //Clear errors
    this.setState({
      firstNameError: false,
      firstNameErrorMessage: '',
      lastNameError: false,
      lastNameErrorMessage: '',
      emailError: false,
      emailErrorMessage: '',
      passwordError: false,
      passwordErrorMessage: '',
      succesful: false
    });

    const err = this.validate();

    if (!err) {
      (async () => {
        try {
          const response = await AuthService.register(
            this.state.firstName,
            this.state.lastName,
            this.state.email,
            this.state.password 
          );
          if (response.status >= 200 && response.status < 300) {
            this.setState({
              //Clear form
              succesful: true,
              formMessage: 'User registered sucessfully.',
              boxcsstype: 'result-box-sucess',
              firstName: '',
              lastName: '',
              email: '',
              password: '',
              redirect: true
            });
          }
        } 
        catch (error) {
          if (error.response.status === 400) {
            this.setState({
              succesful: false,
              formMessage: 'E-mail already in use.',
              boxcsstype: 'result-box-failure'
            });
          }
          else {
            this.setState({
              succesful: false,
              formMessage: 'Database failure.',
              boxcsstype: 'result-box-failure'
            });
            console.log('There was an error!', error)
          }
        }
      })();
    }
  }

  render () {
    const { classes } = this.props;
    return (
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign up
          </Typography>
          <form className={classes.form} noValidate>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                  autoComplete="fname"
                  name="firstName"
                  variant="outlined"
                  required
                  fullWidth
                  id="firstName"
                  label="First Name"
                  autoFocus
                  onChange={this.onChangeFirstName}
                  error={this.state.firstNameError}
                  helperText={this.state.firstNameErrorMessage}
                  value={this.state.firstName}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  variant="outlined"
                  required
                  fullWidth
                  id="lastName"
                  label="Last Name"
                  name="lastName"
                  autoComplete="lname"
                  onChange={this.onChangeLastName}
                  error={this.state.lastNameError}
                  helperText={this.state.lastNameErrorMessage}
                  value={this.state.lastName}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  variant="outlined"
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                  error={this.state.emailError}
                  helperText={this.state.emailErrorMessage}
                  onChange={this.onChangeEmail}
                  value={this.state.email}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  variant="outlined"
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="current-password"
                  error={this.state.passwordError}
                  helperText={this.state.passwordErrorMessage}
                  onChange={this.onChangePassword}
                  value={this.state.password}
                />
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
              onClick={this.onSubmit}
            >
              Sign Up
            </Button>
            <Grid container>
              <Grid item>
                <Link href="/login" variant="body2">
                  Already have an account? Sign in
                </Link>
                {<p className={this.state.boxcsstype}> {this.state.formMessage}</p>}
              </Grid>
            </Grid>
          </form>
          {this.renderRedirect()}
        </div> 
      </Container>
    )
  }
}

export default withStyles(styles)(SignUp);